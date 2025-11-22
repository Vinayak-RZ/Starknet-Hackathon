<div align="center">
    <img width="3421" height="780" alt="title (2)" src="https://github.com/user-attachments/assets/e0e6e83c-3306-42d7-8149-be4ddd1b271f" />
</div>


<div align="center">

![Unity](https://img.shields.io/badge/Unity-2023.3.0f1-000000?style=for-the-badge&logo=unity&logoColor=white)


**A strategic, turn-based war game inspired by the classic Risk**



Players compete for global domination by capturing territories, deploying troops, and outmaneuvering opponents through tactical planning.

This project aims to recreate the fundamental mechanics of Risk while remaining fully customizable and extensible for further development.
</div>
<div>
<div align="center">
    <img width="1533" height="891" alt="Screenshot 2025-05-26 190623" src="https://github.com/user-attachments/assets/71f4565e-a1e8-4c69-b782-e9749559fbdf" />
    <em>Main Menu Screen</em>
</div>

## Game Overview

- **Genre**: Turn-Based Strategy  
- **Players**: Supports 2 to 5 players  
- **Objective**: Conquer the map by controlling all territories  
- **Platform**: PC 

---
<div align="center">
    <img width="1527" height="890" alt="Screenshot 2025-05-25 230632" src="https://github.com/user-attachments/assets/086e136b-202a-4476-9f19-c4b7b59771f7" />
    <em>GamePlay</em>
</div>

## Core Features

- **Randomized Territory Assignment**  
  Territories are evenly and randomly distributed among players at game start.

- **Player Ownership Visualization**  
  Territories change color based on which player owns them, aiding clarity and game flow.

- **Configurable Player Count**  
  Supports 2–5 players with troop allocation rules based on official Risk standards.

- **Territory Intelligence**  
  Each territory tracks its name, owner, troop count, and list of neighboring territories.
  
- **Integrated Enemy AI**  
  Includes a rule-based AI opponent that can make strategic decisions, perform attacks, and reinforce its holdings—ideal for solo play or testing.
  
- **Modular, Extensible Design**  
  Built with ScriptableObjects and serialized C# components for ease of customization and extension.

---

## Tech Stack

- **Unity Engine** (2021.3 LTS or later recommended)
- **C# for game logic**

---
## Future Roadmap

This project is architected with scalability and innovation in mind. In future iterations, we aim to evolve beyond a classic digital board game into a secure, trustless, and decentralized strategy experience through the following enhancements:

- **Blockchain-Backed Randomness**  
  Core mechanics such as dice rolls and event generation will utilize verifiable on-chain randomness (e.g., Chainlink VRF) to guarantee fairness and prevent manipulation.

- **zk-Proof Integration for Game Logic**  
  Zero-knowledge proofs (zk-SNARKs) will be integrated to allow verification of private game logic—such as hidden troop movements or secret strategies—without revealing sensitive information, enabling secure multiplayer logic in a decentralized environment.

- **Web3 Wallet Support**  
  Players will authenticate and interact with the game using non-custodial wallets, allowing for persistent player identity, decentralized matchmaking, and token-based economies.

- **On-Chain Match Logging and Dispute Resolution**  
  Key gameplay events and match outcomes could be recorded on-chain to facilitate tamper-proof auditing, enable transparent leaderboards, and support arbitration in competitive settings.

These features will gradually transform the game into a decentralized, verifiable, and community-owned strategy platform.

