using PaintAndShow.Service.Exceptions;

namespace PaintAndShow.Service.Helpers;

public class PhotoWriter
{
    public string GetPhoto(string photoPath)
    {
        string sourceImagePath = photoPath;

        string saveDirectory = @"..\..\..\..\PaintAndShow.Data\AllMedia\";
        string respons = "";
        try
        {
            //Combine the save directory with the source image file name
            string saveImagePath = Path.Combine(saveDirectory, Path.GetFileName(sourceImagePath));
            respons = saveImagePath;

            //Copy the image from the source path to the save path
            File.Copy(sourceImagePath, saveImagePath, true);

        }
        catch (Exception ex)
        {
            throw new CustomException(400,ex.Message);
        }

        return respons;
    }
}
