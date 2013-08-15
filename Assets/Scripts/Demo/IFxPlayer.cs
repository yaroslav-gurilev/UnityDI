using UnityEngine;

namespace TestDI.Demo
{
	/// <summary>
	/// Интерфейс проигрывателя эффектов
	/// </summary>
	public interface IFxPlayer
	{
		/// <summary>
		/// Проиграть малый взрыв в указанной точке
		/// </summary>
		void PlaySmallBlast(Vector3 pos);

		/// <summary>
		/// Проиграть большой взрыв в указанной точке
		/// </summary>
		void PlayBigBlast(Vector3 getPos);
	}
}
