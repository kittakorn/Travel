using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Helpers;
using TravelApp.Models;
using Xamarin.Forms;

namespace TravelApp.ViewModels
{
    public class PlaceViewModel : BaseViewModel
    {
        public PlaceViewModel()
        {
            Places = new ObservableCollection<Place>();
            LoadPlaceCommand.Execute(null);
        }

        public ObservableCollection<Place> Places { get; set; }

        public Command LoadPlaceCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (IsBusy)
                        return;
                    IsBusy = true;

                    try
                    {
                        Places.Clear();
                        var places = await ApiService.GetPlaceAsync();
                        foreach (var place in places)
                        {
                            Places.Add(place);
                        }
                    }
                    catch (Exception ex)
                    {
                        Toast.Error(ex.Message);
                    }
                    finally
                    {
                        IsBusy = false;
                    }
                });
            }
        }
    }
}
