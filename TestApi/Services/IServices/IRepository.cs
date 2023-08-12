using TestApi.Dto;
using TestApi.Models;

namespace TestApi.Services.IServices
{
    public interface IRepository<T> where T : class
    {
        Task<ResultModel<List<Reservation>>> GetAllReservation();
        Task<ResultModel<Reservation>> GetReservationById(int id);
        Task<ResultModel<Reservation>> DeleteReservation(int id);
        Task<ResultModel<Reservation>> AddReservation(ReservationDTO reservationDTO);
        Task<ResultModel<Reservation>> UpdateReservation(int id,ReservationDTO reservationDTO); 

    }
}
