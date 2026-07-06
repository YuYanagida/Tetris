using Game.Tetris.Common;

public interface IFieldController
{
    public int FieldWidth { get; }
    public int FieldHeight { get; }

    public bool IsObjectType(int x, int y, ObjectType targetType);
    public void ChangeField(int x, int y, ObjectType changeType);
    public ObjectType[,] CreateField(int x, int y);
}
