import time

class Game:
    def __init__(self):
        self.score = 0
        self.obstacles_scaled = 0
        self.running = False

    def start(self):
        self.running = True
        self.score = 0
        self.obstacles_scaled = 0
        print("Game started!")
        self.game_loop()

    def jump(self):
        if self.running:
            self.obstacles_scaled += 1
            print("Player jumped over an obstacle!")

    def game_loop(self):

        start_time = time.time()
        try:
            while self.running:
                time.sleep(0.1)  #Wait for 1/10th of a second
                self.score += 1  #Add 1 point to score for every tenth of a second
               
        except KeyboardInterrupt:
            self.end()

    def end(self):

        self.running = False
        print("Game over!")
        print(f"Total Distance Travelled: {self.score}")
        print(f"Total Obstacles Scaled: {self.obstacles_scaled}")
