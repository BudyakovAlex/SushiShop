using System.Drawing;
using BuildApps.Core.Mobile.MvvmCross.ViewModels.Abstract;
using SushiShop.Core.Data.Enums;
using SushiShop.Core.Data.Models.Stickers;

namespace SushiShop.Core.ViewModels.Menu.Items
{
    public class StickerViewModel : BaseViewModel
    {
        private readonly StickerParams _sticker;

        public StickerViewModel(StickerParams sticker)
        {
            _sticker = sticker;
            _type = sticker.Type;
            _imageUrl = sticker.ImageUrl;
            _backgroundColor = sticker.BackgroundColor;
        }

        private StickerType _type;
        public StickerType Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        private string _imageUrl;
        public string ImageUrl
        {
            get => _imageUrl;
            set => SetProperty(ref _imageUrl, value);
        }
        
        
        private Color _backgroundColor;
        public Color BackgroundColor
        {
            get => _backgroundColor;
            set => SetProperty(ref _backgroundColor, value);
        }
    }
}