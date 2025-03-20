using Microsoft.AspNetCore.Mvc;
using TeloApi.Features.User.DTOs;
using TeloApi.Features.User.Services;

namespace TeloApi.Features.User.Controller;

    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmailVerificationService _emailVerificationService;
        private readonly IUserFavoriteHotelsService _userFavoriteHotelService;

        public UserController(IUserService userService, IEmailVerificationService emailVerificationService, IUserFavoriteHotelsService userFavoriteHotelService)
        {
            _userService = userService;
            _emailVerificationService = emailVerificationService;
            _userFavoriteHotelService = userFavoriteHotelService;
        }

        /// <summary>
        /// Enviar código de verificación por correo
        /// </summary>
        [HttpPost("send-verification-code")]
        public async Task<IActionResult> SendVerificationCode([FromBody] string email)
        {
            try
            {
                var result = await _emailVerificationService.SendVerificationCode(email);
                return result ? Ok(new { message = "Código enviado al correo." }) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Verificar código de verificación enviado al email
        /// </summary>
        [HttpPost("verify-code")]
        public async Task<IActionResult> VerifyCode([FromBody] VerifyCodeRequest request)
        {
            try
            {
                var result = await _emailVerificationService.VerifyCode(request.Email, request.Code);
                return result ? Ok(new { message = "Código verificado correctamente." }) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Registrar un nuevo usuario (solo si verificó su email)
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUser request)
        {
            try
            {
                var result = await _userService.RegisterUser(request);
                return result ? Ok(new { message = "Usuario registrado exitosamente." }) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Actualizar datos del usuario
        /// </summary>
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateUser request)
        {
            try
            {
                var result = await _userService.UpdateUser(request);
                return result ? Ok(new { message = "Usuario actualizado correctamente." }) : BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpPost("add-favorite")]
        public async Task<IActionResult> AddFavorite([FromBody] AddFavorite request)
        {
            try
            {
                var result = await _userFavoriteHotelService.AddFavorite(request);
                return result ? Ok(new { message = "Hotel agregado a favoritos." }) : BadRequest("El hotel ya está en favoritos.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("remove-favorite")]
        public async Task<IActionResult> RemoveFavorite([FromBody] RemoveFavorite request)
        {
            try
            {
                var result = await _userFavoriteHotelService.RemoveFavorite(request);
                return result ? Ok(new { message = "Hotel eliminado de favoritos." }) : BadRequest("El hotel no estaba en favoritos.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("favorites/{userId}")]
        public async Task<IActionResult> GetFavorites(Guid userId)
        {
            try
            {
                var favorites = await _userFavoriteHotelService.GetFavorites(userId);
                return Ok(favorites);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }