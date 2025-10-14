using System.Reflection.Metadata;
using HealthClinic.Models;
using HealthClinic.Models;

namespace HealthClinic.Models;

public abstract class Person
{
    // Private backing fields for Address and Phone
    private string address;
    private string phone;

    // Unique identifier for each person
    public Guid Id { get; set; }

    // Person's first name
    public string Name { get; set; }

    // Type of document (e.g., passport, ID card)
    public string DocumentType { get; set; }

    // Person's last name
    public string LastName { get; set; }

    // Document number or identifier
    public string Document { get; set; }

    // Email address
    public string Email { get; set; }

    // Date of birth
    public DateOnly BirthDay { get; set; }

    // Age in years
    public byte Age { get; set; }

    // Person's address with getter and setter
    public string Address
    {
        get => address;
        set => address = value;
    }

    // Person's phone number with getter and setter
    public string Phone
    {
        get => phone;
        set => phone = value;
    }

    // Constructor to initialize all properties and generate a new unique ID
    public Person(string name,
                    string lastname,
                    string documentType,
                    string document,
                    string email,
                    byte age,
                    string address,
                    string phone,
                    DateOnly birthDay)
    {
        Id = Guid.NewGuid();
        Name = name;
        LastName = lastname;
        DocumentType = documentType;
        Document = document;
        Email = email;
        Age = age;
        Address = address;
        Phone = phone;
        BirthDay = birthDay;
    }

    // Returns a string representation of the person with all main details
    public virtual string ToString()
    {
        return  $@"Id: {Id}
            Name: { Name}
            Lastname: { LastName}
            DocumentType: { DocumentType}
            Document: { Document}
            Email: { Email}
            Age: { Age}
            Address: { Address} 
            Phone: {Phone}";
    }
}