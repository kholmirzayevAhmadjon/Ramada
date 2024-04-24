using FluentValidation;
using Ramada.Service.DTOs.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ramada.Service.Validators.Rooms;

public class RoomCreateModelValidator : AbstractValidator<RoomCreateModel>
{
    public RoomCreateModelValidator()
    {
        RuleFor(room => room.HostelId)
            .NotNull()
            .NotEqual(0)
            .WithMessage(room => $"{nameof(room.HostelId)} is not specified"); 
        
        RuleFor(room => room.RoomNumber)
            .NotNull()
            .NotEqual(0)
            .WithMessage(room => $"{nameof(room.RoomNumber)} is not specified");        
      
        RuleFor(room => room.Price)
            .NotNull()
            .NotEqual(0)
            .WithMessage(room => $"{nameof(room.Price)} is not specified");        
       
        RuleFor(room => room.Floor)
            .NotNull()
            .NotEqual(0)
            .WithMessage(room => $"{nameof(room.Floor)} is not specified");
               
        RuleFor(room => room.MaxPeopleSize)
            .NotNull()
            .NotEqual(0)
            .WithMessage(room => $"{nameof(room.MaxPeopleSize)} is not specified");               
    
        RuleFor(room => room.Size)
            .NotNull()
            .WithMessage(room => $"{nameof(room.Size)} is not specified");          
    
        RuleFor(room => room.Description)
            .NotNull()
            .WithMessage(room => $"{nameof(room.Description)} is not specified");  
        
        RuleFor(room => room.Status)
            .NotNull()
            .WithMessage(room => $"{nameof(room.Status)} is not specified");


        
    }
}
