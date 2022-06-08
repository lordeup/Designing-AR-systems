using System.Collections.Generic;
using System.Linq;
using Card;
using Card.Type;
using UnityEngine;
using Utils;

namespace Storage
{
    public class ImpactPointStorage : MonoBehaviour
    {
        private readonly List<ImpactPoint> _impactPoints = new();
        private readonly List<Impact> _impacts = new();

        public ImpactPointStorage()
        {
            InitializeImpacts();
            InitializeImpactPoints();
        }

        public ImpactPoint GetRandomImpactPoint()
        {
            var item = ArrayUtils.GetRandomListItem(_impactPoints);
            _impactPoints.Remove(item);
            return item;
        }

        private List<Impact> GetImpactsByType(ImpactPointType type)
        {
            return _impacts.Where(item => item.PointType == type).ToList();
        }

        private void InitializeImpactPoints()
        {
            _impactPoints.Add(new ImpactPoint(ImpactPointType.First, GetImpactsByType(ImpactPointType.First),
                "Запустились в Северной Корее, приходи на вечеринку! Кстати, а у вас какие страны в планах?"));
            _impactPoints.Add(new ImpactPoint(ImpactPointType.Second, GetImpactsByType(ImpactPointType.Second),
                "Вчера давал интервью TechCrunch, о тебе тоже рассказал. Журналист спрашивал твой номер"));
            _impactPoints.Add(new ImpactPoint(ImpactPointType.Third, GetImpactsByType(ImpactPointType.Third),
                "Облачный провайдер предложил перевести наши нейросети на их машины со скидкой"));
            _impactPoints.Add(new ImpactPoint(ImpactPointType.Fourth, GetImpactsByType(ImpactPointType.Fourth),
                "Приложение уничтожает половину оценок пользователя, если оценить одно и то же десять раз"));
            _impactPoints.Add(new ImpactPoint(ImpactPointType.Fifth, GetImpactsByType(ImpactPointType.Fifth),
                "Я нашёл облака гораздо дешевле, но компания неизвестная"));
            _impactPoints.Add(new ImpactPoint(ImpactPointType.Sixth, GetImpactsByType(ImpactPointType.Sixth),
                "Тестировщик уже не справляется в одиночку, не так ли?"));
            _impactPoints.Add(new ImpactPoint(ImpactPointType.Seventh, GetImpactsByType(ImpactPointType.Seventh),
                "Вот кнопка-бургер для меню"));
            _impactPoints.Add(new ImpactPoint(ImpactPointType.Eighth, GetImpactsByType(ImpactPointType.Eighth),
                "Сотрудники мало общаются друг с другом, не так ли?"));
            _impactPoints.Add(new ImpactPoint(ImpactPointType.Ninth, GetImpactsByType(ImpactPointType.Ninth),
                "Нашёл классный таск-трекер, нам пригодится"));
            _impactPoints.Add(new ImpactPoint(ImpactPointType.Tenth, GetImpactsByType(ImpactPointType.Tenth),
                "Некто удалил файл с иконкой оценки, она нигде не отображается "));
        }

        private void InitializeImpacts()
        {
            _impacts.Add(new Impact(ImpactPointType.First, ImpactType.Money, 10000, "Только Россия. Сегодня занят"));
            _impacts.Add(new Impact(ImpactPointType.First, ImpactType.Money, -10000,
                "Охватим США и Азию, там посмотрим"));

            _impacts.Add(new Impact(ImpactPointType.Second, ImpactType.Money, -5000, "Нам пока нечего рассказать"));
            _impacts.Add(new Impact(ImpactPointType.Second, ImpactType.Money, 5000, "Записывай"));

            _impacts.Add(new Impact(ImpactPointType.Third, ImpactType.Score, 50, "Давайте"));
            _impacts.Add(new Impact(ImpactPointType.Third, ImpactType.Money, 3000, "Пока не будем"));

            _impacts.Add(new Impact(ImpactPointType.Fourth, ImpactType.Score, -20, "Чиним"));
            _impacts.Add(new Impact(ImpactPointType.Fourth, ImpactType.Money, -2000, "Звучит как редкий кейс"));

            _impacts.Add(new Impact(ImpactPointType.Fifth, ImpactType.Score, -40, "Попробуем перенести часть данных"));
            _impacts.Add(new Impact(ImpactPointType.Fifth, ImpactType.Money, -1000, "Не будем рисковать"));

            _impacts.Add(new Impact(ImpactPointType.Sixth, ImpactType.Money, -2000, "Да, нужен еще один"));
            _impacts.Add(new Impact(ImpactPointType.Sixth, ImpactType.Score, -20, "Не замечал такого"));

            _impacts.Add(new Impact(ImpactPointType.Seventh, ImpactType.Score, -10, "Спасибо, сам нарисую"));
            _impacts.Add(new Impact(ImpactPointType.Seventh, ImpactType.Money, -2500, "Это не тот бургер"));

            _impacts.Add(new Impact(ImpactPointType.Eighth, ImpactType.Score, -15,
                "Вроде достаточно, у нас много чатов"));
            _impacts.Add(new Impact(ImpactPointType.Eighth, ImpactType.Score, 20,
                "Может, есть какие-то облачные решения для интранета?"));

            _impacts.Add(new Impact(ImpactPointType.Ninth, ImpactType.Score, -10, "Да, но стоит дорого"));
            _impacts.Add(new Impact(ImpactPointType.Ninth, ImpactType.Money, -5000, "Если действительно нужно"));

            _impacts.Add(new Impact(ImpactPointType.Tenth, ImpactType.Score, -10, "Пусть нейросеть нарисует новую"));
            _impacts.Add(new Impact(ImpactPointType.Tenth, ImpactType.Money, 3000, " Я заранее закинул все в облако"));
        }
    }
}
