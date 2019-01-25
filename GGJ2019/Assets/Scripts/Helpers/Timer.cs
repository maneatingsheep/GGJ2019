using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    private float _startTime;
    private float _timerDuration;
    private bool _runTimer;
    internal float TimeLeft;
    private Action _onTimeUp;

    public void StartTimer(float duration, Action onTimeUp) {
        _onTimeUp = onTimeUp;
        _runTimer = true;
        _startTime = Time.time;
        _timerDuration = duration;
    }
	
	private void Update () {
        if (!_runTimer) return;
        TimeLeft = _timerDuration - (Time.time - _startTime);
        if(TimeLeft <= 0) {
            TimeUp();
        }
	}

    private void TimeUp() {
        _runTimer = false;
        _onTimeUp();
    }

    internal void StopTimer() {
        _runTimer = false;
    }
}
