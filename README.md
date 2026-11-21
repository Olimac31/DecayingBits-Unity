## License

Decaying Bits uses separate licenses for its source code and creative assets.

- **Source Code:** Licensed under the [Apache License, Version 2.0](LICENSE.txt).
- **Creative Assets and Intellectual Property:** All original characters, artwork, animations, music, sound effects, narrative content, and other creative materials are governed by [LICENSE-ASSETS.md](LICENSE-ASSETS.md).

The Apache License applies exclusively to the project's source code and does not grant rights to its proprietary creative assets.

Please refer to the respective license documents for complete terms and conditions.

## HOW TO OPEN THE PROJECT:
- Install Unity 6.3 LTS
- Clone the repository
- Import it to Unity

<img width="602" height="336" alt="image" src="https://github.com/user-attachments/assets/7f6f17ef-04df-44cf-807d-164cbab6c1a2" />

Once it opens, you gotta click this new "Test" tab at the top of Unity's UI, or else the game will be extremely broken due to Singletons not being initialized.
This tab forces the game to always start on the "gamestart" scene, which loads all Singletons and allows it to work :)
