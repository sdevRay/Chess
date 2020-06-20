# Chess v1.0
### A two-player strategy board game played on a checkered board with 64 squares arranged in an 8×8 grid.

Credits:
- MonoGame tutorials from YouTuber [Oyyou](https://www.youtube.com/channel/UCEXxtHhSvUmz2qcZoWay9kw)
- Graphics Credit to Wikipedia user [Cburnett](https://en.wikipedia.org/wiki/User:Cburnett)

When I first started learning to be a software developer the book [Think Like a Programmer](https://www.amazon.com/Think-Like-Programmer-Introduction-Creative/dp/1593274246) was recommended to me. One of the chapters discussed how the game of Chess can help you logically solve problems. I started playing with some friends, we even set up a little tournament. To pay homage to Chess I tried to make it.

I learned a lot during this project, most notably about design. The original intent was to build a chessboard and have chess pieces traverse it, each with their respected movement parameters. There was never a consideration for the concept of stalemate or checkmate. Implementing these concepts afterwards was challenged and left the end game littered with numerous bugs that only manifest after a unique set of movements. Because of the direction of the original design, a lot of the logic is not optimized very well, I would at least like to thank the Language-Integrated Query (LINQ) for making life a lot easier to get to v1.0.
