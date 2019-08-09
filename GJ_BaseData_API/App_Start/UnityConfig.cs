using System;
using GJ_BaseData_API.Dao;
using GJ_BaseData_API.Dao.LayerDao;
using GJ_BaseData_API.Service;
using GJ_BaseData_API.Service.ServiceImpl;
using Unity;

namespace GJ_BaseData_API
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {

            container.RegisterType<IBusDao, BusDao>();
            container.RegisterType<IBusService, BusServiceImpl>();

            container.RegisterType<IDriverDao, DriverDao>();
            container.RegisterType<IDriverService, DriverServiceImpl>();

            container.RegisterType<ILineDao, LineDao>();
            container.RegisterType<ILineService, LineServiceImpl>();

            container.RegisterType<ILineUDDao, LineUDDao>();
            container.RegisterType<ILineUDService, LineUDServiceImpl>();

            container.RegisterType<ILineStationDao, LineStationDao>();
            container.RegisterType<ILineStationService, LineStationServiceImpl>();

            container.RegisterType<IDepartService, DepartServiceImpl>();
            container.RegisterType<IDepartDao, DepartDao>();

            container.RegisterType<IChangedDataService, ChangedDataServiceImpl>();
            container.RegisterType<IChangedDataDao, ChangedDataDao>();
        }
    }
}