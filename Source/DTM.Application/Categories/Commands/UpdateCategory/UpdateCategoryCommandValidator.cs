namespace DTM.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(category => category.Title).MinimumLength(64).NotEmpty();
            RuleFor(category => category.Colour).NotNull();
        }
    }
}
