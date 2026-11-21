HOW TO OPEN THE PROJECT:
- Install Unity 6.3 LTS
- Clone the repository
- Import it to Unity

<img width="602" height="336" alt="image" src="https://github.com/user-attachments/assets/7f6f17ef-04df-44cf-807d-164cbab6c1a2" />
Once it opens, you gotta click this new "Test" tab at the top of Unity's UI, or else the game will be extremely broken due to Singletons not being initialized.
This tab forces the game to always start on the "gamestart" scene, which loads all Singletons and allows it to work :)
