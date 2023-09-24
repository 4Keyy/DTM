namespace DTM.Application.Notes.Commands.CreateNote
{
    public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
    {
        public CreateNoteCommandValidator()
        {
            RuleFor(note => note.Title).MaximumLength(256).NotEmpty();
        }
    }
}
