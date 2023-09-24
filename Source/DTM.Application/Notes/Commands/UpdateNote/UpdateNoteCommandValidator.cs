namespace DTM.Application.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommandmmandValidator : AbstractValidator<UpdateNoteCommand>
    {
        public UpdateNoteCommandmmandValidator()
        {
            RuleFor(note => note.Title).MaximumLength(256).NotEmpty();
        }
    }
}
