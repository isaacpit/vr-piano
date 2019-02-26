using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class EnemyTracker : SimpleSingleton<EnemyTracker>
{
    Enemy currentTrackingEnemy;

    public void TrackNextEnemy()
    {
        if (EnemyManager.Instance.m_liveEnemies.Count > 0) {
            var index = EnemyManager.Instance.m_liveEnemies.IndexOf(currentTrackingEnemy);
            ++index;
            if (index == EnemyManager.Instance.m_liveEnemies.Count)
            {
                index = 0;
            }
            currentTrackingEnemy = EnemyManager.Instance.m_liveEnemies[index];            
        }
        UpdateMonitorText();        
    }

    private void UpdateMonitorText()
    {
        var chord = currentTrackingEnemy.chord.chordType;
        var note = currentTrackingEnemy.chord.RootNote;
    }
}
