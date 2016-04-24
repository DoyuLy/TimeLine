using System;

namespace Duyu.Timeline.Model
{
    /// <summary>
    /// 根据需要添加元素据
    /// </summary>
    public class ContentModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string ImgUrl { get; set; }
        public DateTime Time { get; set; }
        public string Description { get; set; }
    }
}
