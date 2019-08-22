using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FBPlusOneBuy.DBModels;
using FBPlusOneBuy.Repositories;

namespace FBPlusOneBuy.Services
{
    public class ViewerService
    {
        internal ViewerRepository viewerRepository;

        public ViewerService()
        {
            viewerRepository = new ViewerRepository();
        }
        public void Create(int liveID, string numberOfViewers)
        {
            Viewer viewer = new Viewer()
            {
                LiveID = liveID,
                NumberOfViewers = numberOfViewers
            };
            viewerRepository.Create(viewer);
        }

        public Viewer SearchViewerByLiveID(int liveID)
        {
            var viewers = viewerRepository.GetAll();
            Viewer viewer = new Viewer();
            if (viewers != null)
            {
                viewer = viewers.FirstOrDefault(x => x.LiveID == liveID);
            }
            return viewer;
        }
    }
}