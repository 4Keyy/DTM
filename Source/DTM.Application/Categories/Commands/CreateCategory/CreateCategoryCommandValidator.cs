namespace DTM.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(category => category.Title).MaximumLength(200).NotEmpty();
            RuleFor(category => category.Colour).NotEmpty();
        }
    }
}
