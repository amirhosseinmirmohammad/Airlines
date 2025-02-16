﻿using FlightReservationSystem.Domain.Entities;

public class Flight
{
    public Guid Id { get; set; } 

    public string FlightNumber { get; set; }

    public string Origin { get; set; }

    public string Destination { get; set; }

    public DateTime DepartureTime { get; set; }

    public DateTime ArrivalTime { get; set; }

    public int AvailableSeats { get; set; }

    public ICollection<Reservation> Reservations { get; set; }

    public bool ReserveSeat(int seats = 1)
    {
        if (AvailableSeats >= seats)
        {
            AvailableSeats -= seats;
            return true;
        }
        return false;
    }
}