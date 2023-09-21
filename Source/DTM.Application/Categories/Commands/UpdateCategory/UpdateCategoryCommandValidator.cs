namespace DTM.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(category => category.Title).MinimumLength(20).NotEmpty();
            RuleFor(category => category.Colour).NotNull();
        }
    }
}
