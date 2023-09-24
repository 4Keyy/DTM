namespace DTM.Application.Todos.Commands.CreateTodoItem
{
    public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
    {
        public CreateTodoItemCommandValidator()
        {
            RuleFor(todoItem => todoItem.Title).MaximumLength(256).NotNull();
            RuleFor(todoItem => todoItem.Description).MaximumLength(2048).NotNull();
            RuleFor(todoItem => todoItem.PriorityLevel).NotNull();
        }
    }
}
