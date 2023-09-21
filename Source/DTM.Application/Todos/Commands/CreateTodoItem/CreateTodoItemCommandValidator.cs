namespace DTM.Application.Todos.Commands.CreateTodoItem
{
    public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
    {
        public CreateTodoItemCommandValidator()
        {
            RuleFor(todoItem => todoItem.Title).MaximumLength(200).NotEmpty();
        }
    }
}
