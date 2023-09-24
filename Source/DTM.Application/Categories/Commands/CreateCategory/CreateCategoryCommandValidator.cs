namespace DTM.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(category => category.Title).MaximumLength(64).NotEmpty();
            RuleFor(category => category.Description).MaximumLength(2048);
            RuleFor(category => category.Colour).NotEmpty();
        }
    }
}
