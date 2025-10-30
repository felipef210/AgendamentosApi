
using AgendamentosApi.Dto.Usuario;
using AgendamentosApi.Repository.Usuario;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using UsuarioModel = AgendamentosApi.Models.Usuario;
using System.Text.RegularExpressions;
using AgendamentosApi.Services.Token;

namespace AgendamentosApi.Services.Usuario;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly UserManager<UsuarioModel> _userManager;
    private readonly SignInManager<UsuarioModel> _signInManager;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UsuarioService(IUsuarioRepository usuarioRepository, UserManager<UsuarioModel> userManager, IMapper mapper, SignInManager<UsuarioModel> signInManager, ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
    {
        _usuarioRepository = usuarioRepository;
        _userManager = userManager;
        _mapper = mapper;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<UsuarioDTO> BuscarUsuarioPorId()
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("id")?.Value;

        if (string.IsNullOrEmpty(userId))
            throw new ApplicationException("Usuário não autenticado.");
            
        var usuario = await _userManager.FindByIdAsync(userId);

        if (usuario == null)
            throw new KeyNotFoundException("Usuário não encontrado.");
            
        usuario = await _usuarioRepository.BuscarUsuarioPorId(userId);
        var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);

        return usuarioDTO;
    }

    public async Task Cadastrar([FromBody] CadastroDTO cadastroDTO)
    {
        if (!IsFullName(cadastroDTO.Nome))
            throw new ParametroInvalidoException("Digite seu nome completo para efetuar o cadastro!");

        if (await EmailExiste(cadastroDTO.Email))
            throw new ParametroInvalidoException("E-mail já cadastrado, por favor insira outro e-mail.");

        if (!IsPasswordValid(cadastroDTO.Senha))
            throw new ParametroInvalidoException("Senha no formato inválido!");


        var usuario = _mapper.Map<UsuarioModel>(cadastroDTO);

        usuario.Nome = CapitalizeFullName(usuario.Nome);

        IdentityResult resultado = await _userManager.CreateAsync(usuario, cadastroDTO.Senha);

        if (!resultado.Succeeded)
            throw new ApplicationException("Falha ao cadastrar o usuário!");
    }
    
    public async Task<string> Logar([FromBody] LoginDTO loginDTO)
    {
        var resultado = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Senha, false, false);

        if (!resultado.Succeeded)
            throw new ApplicationException("E-mail ou senha inválido.");

        var usuario = _signInManager
            .UserManager
            .Users
            .FirstOrDefault(u => u.Email == loginDTO.Email);

        var token = _tokenService.GerarToken(usuario!);
        return token;
    }

    public async Task<UsuarioDTO> EditarUsuario([FromBody] EditarPerfilDTO editarPerfilDTO)
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("id")?.Value;

        if (string.IsNullOrEmpty(userId))
            throw new ApplicationException("Usuário não autenticado.");
            
        var usuario = await _userManager.FindByIdAsync(userId);

        if (usuario == null)
            throw new KeyNotFoundException("Usuário não encontrado.");

        if (!IsFullName(editarPerfilDTO.Nome))
            throw new ParametroInvalidoException("Digite seu nome completo para atualizar o perfil!");

        if (editarPerfilDTO.Email != usuario.Email && await EmailExiste(editarPerfilDTO.Email))
            throw new ParametroInvalidoException("E-mail já existente, tente outro endereço");

        usuario.Nome = CapitalizeFullName(editarPerfilDTO.Nome);
        usuario.PhoneNumber = editarPerfilDTO.Telefone;
        usuario.Email = editarPerfilDTO.Email;
        usuario.PasswordHash = usuario.PasswordHash;

        await _usuarioRepository.EditarUsuario(usuario);

        return _mapper.Map<UsuarioDTO>(usuario);
    }

    public async Task<List<UsuarioDTO>> ListarUsuarios()
    {
        var usuarios = await _usuarioRepository.ListarUsuarios();
        var usuariosDTO = _mapper.Map<List<UsuarioDTO>>(usuarios);
        return usuariosDTO;
    }

    private string CapitalizeFullName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            return fullName;

        string[] lowercaseWords = { "da", "de", "do", "dos", "das" };

        return string.Join(" ", fullName
            .ToLower()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select((word, index) =>
            {
                // Sempre mantém a primeira letra do primeiro nome maiuscula, mesmo sendo uma preposição.
                if (index == 0 || !lowercaseWords.Contains(word))
                    return char.ToUpper(word[0]) + word.Substring(1);
                else
                    return word;
            }));
    }

    private bool IsPasswordValid(string password)
    {
        Regex regex = new Regex(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&_*-])[A-Za-z\d!@#$%^&_*-]{8,}$");
        return regex.IsMatch(password);
    }

    private bool IsFullName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            return false;

        string[] separatedName = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        return separatedName.Length > 1;
    }

    private async Task<bool> EmailExiste(string email)
    {
        return await _usuarioRepository.EmailExiste(email);
    }
}
