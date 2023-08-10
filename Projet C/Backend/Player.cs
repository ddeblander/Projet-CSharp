using Projet_C.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

public class Player : User
{
    private int credit;
    private string pseudo;
    private DateTime registrationDate;
    private DateTime dateOfBirth;
    private DateTime date;

    public Player(string pseudo, DateTime rd, DateTime dob)
	{
        Pseudo= pseudo;
        RegistrationDate = rd;  
        DateOfBirth = dob;  
	}


    public int Credit { get; set; }
    public string Pseudo { get; set; }

    public DateTime RegistrationDate { get; set; }
    public DateTime DateOfBirth { get; set; }

}
