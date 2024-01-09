namespace BiblioMag.Models;

public class ReadingSession
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    // (возможность расширения проекта в будущем) Другие свойства для отслеживания прогресса чтения или статистики
}