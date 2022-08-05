using System;
using FluentValidation;

namespace crud_api.Validators
{
    public class AddWalkDifficultyRequestValidator : AbstractValidator<Models.DTO.AddWalkDifficultyRequest>
    {
        public AddWalkDifficultyRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();

        }
    }
}

