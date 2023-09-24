namespace DTM.Application.Todos.Commands.UpdateTodoItem
{
    public class UpdateTodoItemCommandValidator : AbstractValidator<UpdateTodoItemCommand>
    {
        public UpdateTodoItemCommandValidator()
        {
            RuleFor(todoItem => todoItem.Title).MaximumLength(256).NotEmpty();
        }
    }
}
