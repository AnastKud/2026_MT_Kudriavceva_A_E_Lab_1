using System.Numerics;

namespace Matrices;

public sealed class FlatMatrix<T>(int rows, int cols)
    : MatrixBase<T>
    where T : INumber<T>
{
    private readonly T[] data = new T[rows * cols];

    public override int Rows { get; } = rows;

    public override int Cols { get; } = cols;

    public override T this[int row, int col]
    {
        get => this.data[(row * this.Cols) + col];
        set => this.data[(row * this.Cols) + col] = value;
    }

    public IMatrix<T> LoadFromBinaryFile(string path)
    {
        return LoadFromBinaryFileCore(path, (r, c) => new FlatMatrix<T>(r, c));
    }

    protected override IMatrix<T> CreateSameType(int rows, int cols) =>
        new FlatMatrix<T>(rows, cols);
}