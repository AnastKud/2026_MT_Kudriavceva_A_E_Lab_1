using System.Numerics;

namespace Matrices;

public sealed class RectMatrix<T>(int rows, int cols)
    : MatrixBase<T>
    where T : INumber<T>
{
    private readonly T[,] data =
        new T[rows, cols];

    public override int Rows { get; } = rows;

    public override int Cols { get; } = cols;

    public override T this[int row, int col]
    {
        get => this.data[row, col];
        set => this.data[row, col] = value;
    }

    public static IMatrix<T> LoadFromBinaryFile(string path)
    {
        return LoadFromBinaryFileCore(path, (r, c) => new RectMatrix<T>(r, c));
    }

    protected override IMatrix<T> CreateSameType(int rows, int cols) =>
        new RectMatrix<T>(rows, cols);
}