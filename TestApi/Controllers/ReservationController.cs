using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TestApi.Dto;
using TestApi.MiddleWares;
using TestApi.Models;
using TestApi.Services.IServices;

namespace TestApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ReservationController : ControllerBase
{
    private readonly IRepository<Reservation> _reservationRepository;
    private readonly IMapper _mapper;
    private IWebHostEnvironment _webHostEnvironment;
    public ReservationController(IRepository<Reservation> reservationRepository, IMapper mapper, IWebHostEnvironment webHostEnvironment)
    {
        _reservationRepository = reservationRepository;
        _mapper = mapper;
        _webHostEnvironment = webHostEnvironment;
    }

    /// <summary>
    /// this method that retrieves all reservations from a repository.
    /// </summary>
    /// <returns>
    /// IActionResult
    /// </returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllReservation()
    {
        var result = await _reservationRepository.GetAllReservation();
        if (result.Data != null)
        {
            return Ok(result);
        }
        return NotFound();
    }


    /// <summary>
    /// this method is a HttpGet method that retrieves a reservation by Id from a repository
    /// </summary>
    /// <param name="id"></param>
    /// <returns IactionResult></returns>
    /// 

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetReservationById(int id)
    {
        var result = await _reservationRepository.GetReservationById(id);
        if (result.Data != null)
        {
            return Ok(result);
        }
        return (IActionResult)result;
    }


    /// <summary>
    /// This method is a HttpPost method that creates a new reservation in repository
    /// </summary>
    /// <param name="reservationDTO"></param>
    /// <returns>IActionResult</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddReservation([FromBody] ReservationDTO reservationDTO)
    {
        try
        {
            var result = await _reservationRepository.AddReservation(reservationDTO);
            if (result.Data != null)
            {
                return Ok(result);
            }

            return (IActionResult)result;
        }
        catch (Exception)
        {

            throw;
        }
        
    }


    /// <summary>
    ///  This method is a HttpDelete method which is using for removing a reservation from repository
    /// </summary>
    /// <param name="id"></param>
    /// <returns>IActionResult</returns>
    /// 


    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteReservation(int id)
    {

        var result = await _reservationRepository.DeleteReservation(id);
        return (IActionResult)result;

    }


    /// <summary>
    ///  This method is a HttpPatch method that updates a new reservation in repository
    /// </summary>
    /// <param name="id"></param>
    /// <param name="jsonPatch"></param>
    /// <returns>IActionResult</returns>
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdatePartialReservation(int id, JsonPatchDocument<ReservationDTO> jsonPatch)
    {
        var reservationForUpdate = _reservationRepository.GetReservationById(id);
        ReservationDTO reservationForUpdateDTO = _mapper.Map<ReservationDTO>(reservationForUpdate);
        var result = await _reservationRepository.UpdateReservation(id, reservationForUpdateDTO);
        if (result.IsSuccess)
        {
            jsonPatch.ApplyTo(reservationForUpdateDTO, ModelState);
            return Ok(result);
        }
        return BadRequest();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateReservation(int id, [FromBody] ReservationDTO reservationDTO)
    {
        try
        {

            var result = await _reservationRepository.UpdateReservation(id, reservationDTO);
            if (result.IsSuccess)
            {
                return NoContent();
            }
            return BadRequest();
        }
        catch (Exception)
        {

            throw;
        }

    }

    [HttpPost("UploadFile")]
    public async Task<string> UploadFile([FromForm] IFormFile file)
    {
        string path = Path.Combine(_webHostEnvironment.WebRootPath, "Images/"
            + file.FileName);
        using (Stream stream = new FileStream(path, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }
        return "https://localhost:44385/Images/" + file.FileName;
    }
}
