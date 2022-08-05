using System;
using FluentValidation;
namespace crud_api.Validators
{
    public class UpdateWalkDifficultyRequestValidator: AbstractValidator<Models.DTO.UpdateWalkDifficultyRequest>
    {
        public UpdateWalkDifficultyRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();

        }
    }
}

