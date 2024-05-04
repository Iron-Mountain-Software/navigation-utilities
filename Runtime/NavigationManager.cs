using System;
using System.Collections.Generic;
using UnityEngine;

namespace IronMountain.NavigationUtilities
{
    public class NavigationManager : MonoBehaviour
    {
        #region Waypoints
        public static event Action OnWaypointsChanged;
        public static readonly List<Waypoint> Waypoints = new ();

        public static void Register(Waypoint waypoint)
        {
            if (!waypoint || Waypoints.Contains(waypoint)) return; 
            Waypoints.Add(waypoint);
            OnWaypointsChanged?.Invoke();
        }

        public static void Unregister(Waypoint waypoint)
        {
            if (!waypoint || !Waypoints.Contains(waypoint)) return; 
            Waypoints.Remove(waypoint);
            OnWaypointsChanged?.Invoke();
        }

        public static Waypoint GetWaypointByID(string id)
        {
            return !string.IsNullOrWhiteSpace(id) 
                ? Waypoints.Find(test => test && test.ID == id) 
                : null;
        }
        
        public static Waypoint GetWaypointByName(string name)
        {
            return !string.IsNullOrWhiteSpace(name)
                ? Waypoints.Find(test => test && test.name == name)
                : null;
        }

        #endregion

        #region Zones
        public static event Action OnZonesChanged;
        public static readonly List<Zone> Zones = new ();

        public static void Register(Zone zone)
        {
            if (!zone || Zones.Contains(zone)) return; 
            Zones.Add(zone);
            OnZonesChanged?.Invoke();
        }

        public static void Unregister(Zone zone)
        {
            if (!zone || !Zones.Contains(zone)) return; 
            Zones.Remove(zone);
            OnZonesChanged?.Invoke();
        }

        public static Zone GetZoneByID(string id)
        {
            return !string.IsNullOrWhiteSpace(id) 
                ? Zones.Find(test => test && test.ID == id) 
                : null;
        }
        
        public static Zone GetZoneByName(string name)
        {
            return !string.IsNullOrWhiteSpace(name)
                ? Zones.Find(test => test && test.name == name)
                : null;
        }
        #endregion

        #region Trackers
        public static event Action OnTrackersChanged;
        public static readonly List<ZoneTracker> Trackers = new();

        public static void Register(ZoneTracker ambientZoneTracker)
        {
            if (!ambientZoneTracker || Trackers.Contains(ambientZoneTracker)) return;
            Trackers.Add(ambientZoneTracker);
            OnTrackersChanged?.Invoke();
        }
        
        public static void Unregister(ZoneTracker ambientZoneTracker)
        {
            if (!ambientZoneTracker || !Trackers.Contains(ambientZoneTracker)) return;
            Trackers.Remove(ambientZoneTracker);
            OnTrackersChanged?.Invoke();
        }
        #endregion
    }
}