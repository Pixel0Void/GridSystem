using UnityEngine;

public struct Boundary
{
	private int m_PositiveX, m_NegativeX;
	private int m_PositiveZ, m_NegativeZ;

	public Boundary(int gridSize, int cellSize)
	{
		m_NegativeX = 0;
		m_NegativeZ = 0;

		m_PositiveX = (gridSize / 2) * (cellSize / 2);
		m_NegativeX -= m_PositiveX;
		m_PositiveZ = (gridSize / 2) * (cellSize / 2);
        m_NegativeZ -= m_PositiveZ;
    }

    public bool IsInBound(Vector3 pos)
	{
		return pos.x >= m_NegativeX && pos.x <= m_PositiveX && pos.z >= m_NegativeZ && pos.z <= m_PositiveZ;
	}
}
