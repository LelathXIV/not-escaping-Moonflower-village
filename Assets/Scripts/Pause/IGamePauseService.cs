interface IGamePauseService
{
    bool isFrozen { get; set; }
    void Freeze();
    void UnFreeze();
}
