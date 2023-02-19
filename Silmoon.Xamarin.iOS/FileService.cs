using CoreFoundation;
using Foundation;
using Photos;
using Silmoon.Xamarin.Interfaces;
using Silmoon.Xamarin.iOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileService))]
namespace Silmoon.Xamarin.iOS
{
    public class FileService : IFileService
    {
        public FileService()
        {

        }
        public void SaveImage(string name, byte[] data, Action<bool> callback, string albumName = null)
        {
            if (albumName == null || albumName == string.Empty)
            {
                var imageData = new UIImage(NSData.FromArray(data));
                imageData.SaveToPhotosAlbum((image, error) => callback(error == null));
            }
            else
            {
                var fetchOptions = new PHFetchOptions();
                fetchOptions.Predicate = NSPredicate.FromFormat("localizedTitle = %@", new NSString(albumName));
                var collections = PHAssetCollection.FetchAssetCollections(PHAssetCollectionType.Album, PHAssetCollectionSubtype.Any, fetchOptions);
                var assetCollection = (PHAssetCollection)collections.FirstOrDefault();


                if (assetCollection == null)
                {
                    PHPhotoLibrary.SharedPhotoLibrary.PerformChanges(() => PHAssetCollectionChangeRequest.CreateAssetCollection(albumName), (success, err) =>
                    {
                        if (!success) callback(false);
                        else
                        {
                            collections = PHAssetCollection.FetchAssetCollections(PHAssetCollectionType.Album, PHAssetCollectionSubtype.Any, fetchOptions);
                            assetCollection = (PHAssetCollection)collections.FirstOrDefault();
                            if (assetCollection != null)
                            {
                                var imageData = NSData.FromArray(data);
                                var image = UIImage.LoadFromData(imageData);

                                PHPhotoLibrary.SharedPhotoLibrary.PerformChanges(() =>
                                {
                                    var assetChangeRequest = PHAssetChangeRequest.FromImage(image);
                                    var albumChangeRequest = PHAssetCollectionChangeRequest.ChangeRequest(assetCollection);
                                    albumChangeRequest.AddAssets(new PHObject[] { assetChangeRequest.PlaceholderForCreatedAsset });
                                }, (success2, error) => DispatchQueue.MainQueue.DispatchAsync(() => callback(success2 && error == null)));
                            }
                            else callback(false);
                        }
                    });
                }
                else if (assetCollection != null)
                {
                    var imageData = NSData.FromArray(data);
                    var image = UIImage.LoadFromData(imageData);

                    PHPhotoLibrary.SharedPhotoLibrary.PerformChanges(() =>
                    {
                        var assetChangeRequest = PHAssetChangeRequest.FromImage(image);
                        var albumChangeRequest = PHAssetCollectionChangeRequest.ChangeRequest(assetCollection);
                        albumChangeRequest.AddAssets(new PHObject[] { assetChangeRequest.PlaceholderForCreatedAsset });
                    }, (success, error) => DispatchQueue.MainQueue.DispatchAsync(() => callback(success && error == null)));
                }
                else callback(false);
            }
        }
    }
}