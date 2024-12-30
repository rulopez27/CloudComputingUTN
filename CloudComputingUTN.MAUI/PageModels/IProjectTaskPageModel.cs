using CloudComputingUTN.MAUI.Models;
using CommunityToolkit.Mvvm.Input;

namespace CloudComputingUTN.MAUI.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}