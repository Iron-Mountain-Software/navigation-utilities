using System;
using System.Collections.Generic;
using UnityEngine;

namespace IronMountain.NavigationUtilities
{
    public class ZoneTracker : NavigationElement
    {
        public static event Action<ZoneTracker> OnAnyZoneTrackerChanged;
        public event Action OnZoneTrackerChanged;

        [SerializeField] private List<Zone> currentZones = new();
        
        public List<Zone> CurrentZones => currentZones;

        protected virtual void OnEnable() => NavigationManager.Register(this);
        protected virtual void OnDisable() => NavigationManager.Unregister(this);
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Zone ambientZone))
            {
                Enter(ambientZone);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Zone ambientZone))
            {
                Exit(ambientZone);
            }
        }
        
        private void Enter(Zone zone)
        {
            if (!zone || currentZones.Contains(zone)) return;
            currentZones.Add(zone);
            OnZoneTrackerChanged?.Invoke();
            OnAnyZoneTrackerChanged?.Invoke(this);
        }

        private void Exit(Zone zone)
        {
            if (!zone || !currentZones.Contains(zone)) return;
            currentZones.Remove(zone);
            OnZoneTrackerChanged?.Invoke();
            OnAnyZoneTrackerChanged?.Invoke(this);
        }
    }
}
