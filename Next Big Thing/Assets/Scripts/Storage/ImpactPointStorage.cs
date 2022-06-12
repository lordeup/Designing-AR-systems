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
        private readonly List<ImpactValue> _impactValues = new();
        private readonly List<Impact> _impacts = new();
        private readonly List<ImpactPoint> _impactPoints = new();

        public ImpactPointStorage()
        {
            InitializeImpactValues();
            InitializeImpacts();
            InitializeImpactPoints();
        }

        public ImpactPoint GetRandomImpactPoint()
        {
            var item = ArrayUtils.GetRandomListItem(_impactPoints);
            _impactPoints.Remove(item);
            return item;
        }

        public List<Impact> GetImpactsByType(ImpactPointType type)
        {
            return _impacts.Where(item => item.PointType == type).ToList();
        }

        private void InitializeImpactValues()
        {
            AddImpactValue(ImpactPointType._1, ImpactValueType._1, ImpactType.Money, 1000);
            AddImpactValue(ImpactPointType._1, ImpactValueType._2, ImpactType.Money, -1000);
            AddImpactValue(ImpactPointType._2, ImpactValueType._1, ImpactType.Money, -500);
            AddImpactValue(ImpactPointType._2, ImpactValueType._2, ImpactType.Money, 600);
            AddImpactValue(ImpactPointType._3, ImpactValueType._1, ImpactType.Score, 15);
            AddImpactValue(ImpactPointType._3, ImpactValueType._2, ImpactType.Money, 1000);
            AddImpactValue(ImpactPointType._4, ImpactValueType._1, ImpactType.Score, -20);
            AddImpactValue(ImpactPointType._4, ImpactValueType._2, ImpactType.Money, -500);
            AddImpactValue(ImpactPointType._5, ImpactValueType._1, ImpactType.Score, -15);
            AddImpactValue(ImpactPointType._5, ImpactValueType._2, ImpactType.Money, -1500);
            AddImpactValue(ImpactPointType._6, ImpactValueType._1, ImpactType.Money, -1000);
            AddImpactValue(ImpactPointType._6, ImpactValueType._2, ImpactType.Score, -10);
            AddImpactValue(ImpactPointType._7, ImpactValueType._1, ImpactType.Score, -10);
            AddImpactValue(ImpactPointType._7, ImpactValueType._2, ImpactType.Money, -2500);
            AddImpactValue(ImpactPointType._8, ImpactValueType._1, ImpactType.Score, -10);
            AddImpactValue(ImpactPointType._8, ImpactValueType._2, ImpactType.Score, 15);
            AddImpactValue(ImpactPointType._9, ImpactValueType._1, ImpactType.Score, -20);
            AddImpactValue(ImpactPointType._9, ImpactValueType._2, ImpactType.Money, -10000);
            AddImpactValue(ImpactPointType._9, ImpactValueType._2, ImpactType.Score, 25);
            AddImpactValue(ImpactPointType._10, ImpactValueType._1, ImpactType.Score, -25);
            AddImpactValue(ImpactPointType._10, ImpactValueType._2, ImpactType.Money, 6000);
            AddImpactValue(ImpactPointType._11, ImpactValueType._1, ImpactType.Score, -15);
            AddImpactValue(ImpactPointType._11, ImpactValueType._2, ImpactType.Score, 20);
            AddImpactValue(ImpactPointType._12, ImpactValueType._1, ImpactType.Money, -4000);
            AddImpactValue(ImpactPointType._12, ImpactValueType._2, ImpactType.Money, -7000);
            AddImpactValue(ImpactPointType._12, ImpactValueType._2, ImpactType.Score, 20);
            AddImpactValue(ImpactPointType._13, ImpactValueType._1, ImpactType.Money, -4500);
            AddImpactValue(ImpactPointType._13, ImpactValueType._2, ImpactType.Money, 5000);
            AddImpactValue(ImpactPointType._14, ImpactValueType._1, ImpactType.Score, -20);
            AddImpactValue(ImpactPointType._14, ImpactValueType._2, ImpactType.Money, -6500);
            AddImpactValue(ImpactPointType._15, ImpactValueType._1, ImpactType.Score, -10);
            AddImpactValue(ImpactPointType._15, ImpactValueType._2, ImpactType.Money, -7000);
            AddImpactValue(ImpactPointType._16, ImpactValueType._1, ImpactType.Score, -15);
            AddImpactValue(ImpactPointType._16, ImpactValueType._2, ImpactType.Money, 9000);
            AddImpactValue(ImpactPointType._16, ImpactValueType._2, ImpactType.Score, -5);
            AddImpactValue(ImpactPointType._17, ImpactValueType._1, ImpactType.Money, -5000);
            AddImpactValue(ImpactPointType._17, ImpactValueType._1, ImpactType.Score, 15);
            AddImpactValue(ImpactPointType._17, ImpactValueType._2, ImpactType.Score, -20);
            AddImpactValue(ImpactPointType._18, ImpactValueType._1, ImpactType.Money, -5000);
            AddImpactValue(ImpactPointType._18, ImpactValueType._1, ImpactType.Score, 20);
            AddImpactValue(ImpactPointType._18, ImpactValueType._2, ImpactType.Score, -25);
            AddImpactValue(ImpactPointType._19, ImpactValueType._1, ImpactType.Score, -15);
            AddImpactValue(ImpactPointType._19, ImpactValueType._2, ImpactType.Money, -2000);
            AddImpactValue(ImpactPointType._20, ImpactValueType._1, ImpactType.Score, 25);
            AddImpactValue(ImpactPointType._20, ImpactValueType._2, ImpactType.Score, -20);
            AddImpactValue(ImpactPointType._21, ImpactValueType._1, ImpactType.Money, 1000);
            AddImpactValue(ImpactPointType._21, ImpactValueType._2, ImpactType.Score, -10);
            AddImpactValue(ImpactPointType._22, ImpactValueType._1, ImpactType.Score, 15);
            AddImpactValue(ImpactPointType._22, ImpactValueType._2, ImpactType.Score, -20);
            AddImpactValue(ImpactPointType._23, ImpactValueType._1, ImpactType.Score, -15);
            AddImpactValue(ImpactPointType._23, ImpactValueType._2, ImpactType.Money, -5500);
            AddImpactValue(ImpactPointType._24, ImpactValueType._1, ImpactType.Money, -8000);
            AddImpactValue(ImpactPointType._24, ImpactValueType._2, ImpactType.Money, 10000);
            AddImpactValue(ImpactPointType._25, ImpactValueType._1, ImpactType.Score, -25);
            AddImpactValue(ImpactPointType._25, ImpactValueType._2, ImpactType.Money, 10000);
            AddImpactValue(ImpactPointType._25, ImpactValueType._2, ImpactType.Score, -10);
        }

        private void InitializeImpacts()
        {
            AddImpact(ImpactPointType._1, ImpactValueType._1, "Только Россия. Сегодня занят");
            AddImpact(ImpactPointType._1, ImpactValueType._2, "Охватим США и Азию, там посмотрим");
            AddImpact(ImpactPointType._2, ImpactValueType._1, "Нам пока нечего рассказать");
            AddImpact(ImpactPointType._2, ImpactValueType._2, "Записывай");
            AddImpact(ImpactPointType._3, ImpactValueType._1, "Давайте");
            AddImpact(ImpactPointType._3, ImpactValueType._2, "Пока не будем");
            AddImpact(ImpactPointType._4, ImpactValueType._1, "Чиним");
            AddImpact(ImpactPointType._4, ImpactValueType._2, "Звучит как редкий кейс");
            AddImpact(ImpactPointType._5, ImpactValueType._1, "Попробуем перенести часть данных");
            AddImpact(ImpactPointType._5, ImpactValueType._2, "Не будем рисковать");
            AddImpact(ImpactPointType._6, ImpactValueType._1, "Да, нужен еще один");
            AddImpact(ImpactPointType._6, ImpactValueType._2, "Не замечал такого");
            AddImpact(ImpactPointType._7, ImpactValueType._1, "Спасибо, сам нарисую");
            AddImpact(ImpactPointType._7, ImpactValueType._2, "Это не тот бургер");
            AddImpact(ImpactPointType._8, ImpactValueType._1, "Вроде достаточно, у нас много чатов");
            AddImpact(ImpactPointType._8, ImpactValueType._2, "Может, есть какие-то облачные решения для интранета?");
            AddImpact(ImpactPointType._9, ImpactValueType._1, "Да, но стоит дорого");
            AddImpact(ImpactPointType._9, ImpactValueType._2, "Если действительно нужно");
            AddImpact(ImpactPointType._10, ImpactValueType._1, "Пусть нейросеть нарисует новую");
            AddImpact(ImpactPointType._10, ImpactValueType._2, "Я заранее закинул все в облако");
            AddImpact(ImpactPointType._11, ImpactValueType._1, "Не критично");
            AddImpact(ImpactPointType._11, ImpactValueType._2, "Так не пойдет");
            AddImpact(ImpactPointType._12, ImpactValueType._1, "Ладно");
            AddImpact(ImpactPointType._12, ImpactValueType._2, "Нам тоже надо");
            AddImpact(ImpactPointType._13, ImpactValueType._1, "Ладно");
            AddImpact(ImpactPointType._13, ImpactValueType._2, "Этого не было в договоре");
            AddImpact(ImpactPointType._14, ImpactValueType._1, "Подождем, пока примут");
            AddImpact(ImpactPointType._14, ImpactValueType._2, "Подготовимся заранее");
            AddImpact(ImpactPointType._15, ImpactValueType._1, "Найдем офис");
            AddImpact(ImpactPointType._15, ImpactValueType._2, "Остаемся");
            AddImpact(ImpactPointType._16, ImpactValueType._1, "Сначала запустимся, там решим");
            AddImpact(ImpactPointType._16, ImpactValueType._2, "Сделаем платные функции");
            AddImpact(ImpactPointType._17, ImpactValueType._1, "Конечно");
            AddImpact(ImpactPointType._17, ImpactValueType._2, "Это глупости");
            AddImpact(ImpactPointType._18, ImpactValueType._1, "Но здесь же ничего нет");
            AddImpact(ImpactPointType._18, ImpactValueType._2, "Зато быстро сделаете");
            AddImpact(ImpactPointType._19, ImpactValueType._1, "Еще рано");
            AddImpact(ImpactPointType._19, ImpactValueType._2, "Да, пора");
            AddImpact(ImpactPointType._20, ImpactValueType._1, "Переориентируем бизнес");
            AddImpact(ImpactPointType._20, ImpactValueType._2, "Это несерьезно");
            AddImpact(ImpactPointType._21, ImpactValueType._1, "Это не так. Но спасибо за замечание");
            AddImpact(ImpactPointType._21, ImpactValueType._2, "Я не собираюсь ничего вам доказывать");
            AddImpact(ImpactPointType._22, ImpactValueType._1, "Да, сотрудникам нравится");
            AddImpact(ImpactPointType._22, ImpactValueType._2, "Нет, это не наш");
            AddImpact(ImpactPointType._23, ImpactValueType._1, "Ладно");
            AddImpact(ImpactPointType._23, ImpactValueType._2, "Вы это компенсируете?");
            AddImpact(ImpactPointType._24, ImpactValueType._1, "Ты уверен?");
            AddImpact(ImpactPointType._24, ImpactValueType._2, "Поделишься какими-нибудь приемами?");
            AddImpact(ImpactPointType._25, ImpactValueType._1, "Извини, нет времени");
            AddImpact(ImpactPointType._25, ImpactValueType._2, "Только доделаю пару вещей");
        }

        private void InitializeImpactPoints()
        {
            AddImpactPoint(ImpactPointType._1,
                "Запустились в Северной Корее, приходи на вечеринку! Кстати, а у вас какие страны в планах?");
            AddImpactPoint(ImpactPointType._2,
                "Вчера давал интервью TechCrunch, о тебе тоже рассказал. Журналист спрашивал твой номер");
            AddImpactPoint(ImpactPointType._3,
                "Облачный провайдер предложил перевести наши нейросети на их машины со скидкой");
            AddImpactPoint(ImpactPointType._4,
                "Приложение уничтожает половину оценок пользователя, если оценить одно и то же десять раз");
            AddImpactPoint(ImpactPointType._5, "Я нашёл облака гораздо дешевле, но компания неизвестная");
            AddImpactPoint(ImpactPointType._6, "Тестировщик уже не справляется в одиночку, не так ли?");
            AddImpactPoint(ImpactPointType._7, "Вот кнопка-бургер для меню");
            AddImpactPoint(ImpactPointType._8, "Сотрудники мало общаются друг с другом, не так ли?");
            AddImpactPoint(ImpactPointType._9, "Нашёл классный таск-трекер, нам пригодится");
            AddImpactPoint(ImpactPointType._10, "Некто удалил файл с иконкой оценки, она нигде не отображается");
            AddImpactPoint(ImpactPointType._11, "Приложение не отделяет кошек и собак");
            AddImpactPoint(ImpactPointType._12,
                "О, ты с обеда? А мы больше в местное кафе не ходим, наняли повара. Очень дорого, даже рассказывать не буду");
            AddImpactPoint(ImpactPointType._13, "Завтра у нас мероприятие, приходить нельзя. Деньги не вернем");
            AddImpactPoint(ImpactPointType._14,
                "Я слышал, готовится закон, по которому нам придётся хранить больше данных");
            AddImpactPoint(ImpactPointType._15, "Со следующего месяца мы повышаем стоимость аренды");
            AddImpactPoint(ImpactPointType._16, "На чём будете зарабатывать? Только не предлагай рекламу");
            AddImpactPoint(ImpactPointType._17, "Мы забыли главное. Можно ли оценивать котиков?");
            AddImpactPoint(ImpactPointType._18, "Вот самый простой вариант экрана оценки");
            AddImpactPoint(ImpactPointType._19,
                "В облаке скидки для компаний, которые работают в нескольких странах. Вы планировали выходить в новые?");
            AddImpactPoint(ImpactPointType._20, "Пользователи используют наш сервис только для оценок животных");
            AddImpactPoint(ImpactPointType._21, "Все оценки лимонадов проплачены, я уверен");
            AddImpactPoint(ImpactPointType._22, "Ты потратил деньги на повара?");
            AddImpactPoint(ImpactPointType._23, "На кухне ремонт, несколько недель придётся ходить на другой этаж");
            AddImpactPoint(ImpactPointType._24,
                "Поздравляю с запуском! Как дела с аудиторией? Мы набрали первый миллион пользователей за неделю");
            AddImpactPoint(ImpactPointType._25,
                "Иду на презентацию того облачного провайдера, говорят там будут все шишки. Ты со мной?");
        }

        private void AddImpactValue(ImpactPointType pointType, ImpactValueType valueType, ImpactType impactType,
            double value)
        {
            _impactValues.Add(new ImpactValue(pointType, valueType, impactType, value));
        }

        private void AddImpact(ImpactPointType pointType, ImpactValueType valueType, string description)
        {
            var values = GetImpactValuesByType(pointType, valueType);
            _impacts.Add(new Impact(pointType, values, description));
        }

        private void AddImpactPoint(ImpactPointType pointType, string description)
        {
            _impactPoints.Add(new ImpactPoint(pointType, description));
        }

        private List<ImpactValue> GetImpactValuesByType(ImpactPointType pointType, ImpactValueType valueType)
        {
            return _impactValues.Where(item => item.PointType == pointType && item.ValueType == valueType).ToList();
        }
    }
}
