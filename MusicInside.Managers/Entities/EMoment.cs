using MusicInside.Models.Models;

namespace MusicInside.Managers.Entities
{
    public class EMoment
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public void CopyFromModel(Moment moment)
        {
            this.Id = moment.Id;
            this.Description = moment.Description;
        }
    }
}
