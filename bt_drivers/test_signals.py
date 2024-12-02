import time

def test_signals():
    """
    Simulates sending keypresses based on time intervals.
    Returns:
    - 0 most of the time (no keypress)
    - 1, 2, or 3 at specific time intervals.
    """
    current_time = int(time.time())  # Get the current time in seconds
    if current_time % 2 == 0:  # Every 10 seconds
        return 99
    elif current_time % 3 == 0:  # Every 15 seconds
        return 98
    elif current_time % 5 == 0:  # Every 20 seconds
        return 97
    else:
        return 0
