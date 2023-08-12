
using Microsoft.AspNetCore.Http.HttpResults;
using TestApi.Dto;
using TestApi.Data;
using TestApi.Models;
using TestApi.Services.IServices;

namespace TestApi.Services;

public class Repository<T> : IRepository<T> where T : class
{
    private InputData input;
    public Repository(InputData inputData)
    {
        input = inputData;

    }

    ResultModel<Reservation> result = new ResultModel<Reservation>();


    public async Task<ResultModel<Reservation>> AddReservation(ReservationDTO reservationDTO)
    {
        List<Reservation> reservationList = input.ReadJsonFile();

        try
        {
            if (reservationDTO.Name == null || reservationDTO.StartLocation == null || reservationDTO.EndLocation == null)
            {
                result.SetResultProperties(false, null, new List<object> { "Invalid reservation data" });
                return result;
            }

            Reservation newReservation = new Reservation
            {
                Name = reservationDTO.Name,
                StartLocation = reservationDTO.StartLocation,
                EndLocation = reservationDTO.EndLocation
            };

            if (newReservation == null)
            {
                result.SetResultProperties(false, null, new List<object> { "New reservation item cannot be empty!" });
                return result;
            }

            reservationList.Add(newReservation);

            result.SetResultProperties(true, newReservation);
            return result;


        }
        catch (Exception ex)
        {
            result.SetResultProperties(false, null, new List<object> { ex.Message });
            return result;
        }
    }




    public async Task<ResultModel<Reservation>> DeleteReservation(int id)
    {
        List<Reservation> reservationList = input.ReadJsonFile();
        try
        {
            if (id == 0)
            {
                result.SetResultProperties(false, null, new List<object> { "Id field can't be empty!" });
                return result;
            }
            var reservationForDelete = reservationList.FirstOrDefault(x => x.Id == id);

            if (reservationForDelete == null)
            {
                result.SetResultProperties(false, null, new List<object> { "Reservation for delete doesn't exist" });
                return result;
            }

            reservationList.Remove(reservationForDelete);
            result.SetResultProperties(true, reservationForDelete);
            return result;
        }
        catch (Exception ex)
        {

            result.SetResultProperties(false, null, new List<object> { ex.Message });
            return result;
        }
    }

    public async Task<ResultModel<List<Reservation>>> GetAllReservation()
    {
        ResultModel<List<Reservation>> resultModel = new ResultModel<List<Reservation>>();
        List<Reservation> reservationList = input.ReadJsonFile();
        try
        {
            List<Reservation> reservations = reservationList;
            if (reservations.Count() == 0)
            {
                resultModel.SetResultProperties(false, reservations, new List<object> { "Reservation list is empty!" });
                return resultModel;
            }

            resultModel.SetResultProperties(true, reservations);
            return resultModel;
        }
        catch (Exception ex)
        {

            resultModel.SetResultProperties(false, null, new List<object> { ex.Message });
            return resultModel;
        }
    }



    public async Task<ResultModel<Reservation>> GetReservationById(int id)
    {
        List<Reservation> reservationList = input.ReadJsonFile();
        try
        {
            var reservation = reservationList.Find(x => x.Id == id);
            if (reservation == null)
            {
                result.SetResultProperties(false, null, new List<object> { "Reservation doesn't exist" });
                return result;
            }
            result.SetResultProperties(true, reservation);
            return result;
        }
        catch (Exception ex)
        {

            result.SetResultProperties(false, null, new List<object> { ex.Message });
            return result;
        }
    }

    public async Task<ResultModel<Reservation>> UpdateReservation(int id, ReservationDTO reservationDTO)
    {
        List<Reservation> reservationList = input.ReadJsonFile();
        try
        {
            var reservation = reservationList.Find(x => x.Id == id);
            if (reservation == null)
            {
                result.SetResultProperties(false, null, new List<object> { "Reservation doesn't exist" });
                return result;
            }
            reservation.Name = reservationDTO.Name;
            reservation.StartLocation = reservationDTO.StartLocation;
            reservation.EndLocation = reservationDTO.EndLocation;
            result.SetResultProperties(true, reservation);
            return result;
        }
        catch (Exception ex)
        {
            result.SetResultProperties(false, null, new List<object> { ex.Message });
            return result;
        }
    }
}
