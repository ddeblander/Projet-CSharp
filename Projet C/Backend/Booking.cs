using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class Booking
{
	private DateTime bookingDate;
	public Booking(DateTime bd)
	{
		BookingDate= bd;	
	}
	public void Delete()
	{
        //BookingDate= 0;

    }
	public DateTime BookingDate { get; set; }
}
