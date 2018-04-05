using System.Collections.Generic;

public static class HelperTextData
{

    public static string[] GetRussian(int index)
    {
        switch (index)
        {
            case 3:
                return new string[] {
                    "Добро пожаловать в Solys! Нажмите на эту подсказку для продолжения.",
                    "Нарисуйте линию, чтобы диск попал в портал.",
                    "После этого запустите симуляцию, нажав на кнопку в левом нижнем углу.",
                    "Вы можете использовать ластик, чтобы исправить небольшие недочёты.",
                    "Нажмите на ластик трижды, чтобы удалить все линии."
                };
            case 5:
                return new string[]{
                    "Для рисования линий тратится шкала заряда.",
                    "Ваш результат зависит от количества потраченнго заряда.",
                    "Чем больше заряда останется - тем лучше!"
                };
            case 6:
                return new string[] {
                    "Коснитесь тёмного блока во время симуляции."
                };
            case 7:
                return new string[] {
                    "Каждый запуск симуляции тратит одно очко энергии.",
                    "Вы можете увидеть оставшуюся энегию под кнопкой в левом нижнем углу.",
                    "Но энегию не сложно восстановить.",
                    "Не бойтесь эксперементировать!"
                };
            case 8:
                return new string[] {
                    "Испоьзуйте два пальца, чтобы перемещать камеру."
                };
            case 10:
                return new string[] {
                    "Познакомьтесь с блоком Гравитации!",
                    "Когда диск пролетает мимо этого блока - направление гравитации меняется."
                };
            case 14:
                return new string[] {
                    "А это блок Силы.",
                    "Он толкает диск в по направлению стрелки."
                };
            case 16:
                return new string[] {
                    "А это блок Ускорения.",
                    "Пролетая мимо этого блока диск ускоряется"
                };
            case 20:
                return new string[] {
                    "Блок Телепорта.",
                    "Диск перемещается от входа телепорта к выходу."
                };

            default:
                return new string[0];
        }



    }


    public static string[] GetEnglish(int index)
    {
        switch (index)
        {
            case 3:
                return new string[] {
                    "Welcome to Solys! Click on this card to continue.",
                    "Draw a line to make the disk hit the portal.",
                    "After that, run the simulation by clicking on the button in the lower left corner.",
                    "You can use the eraser to fix minor shortcomings.",
                    "Click on the eraser three times to remove all the lines."
                };
            case 5:
                return new string[]{
                    "The charge scale is spent on drawing lines.",
                    "Your result depends on the amount of charge spent.",
                    "Less charge spent - more stars!"
                };
            case 6:
                return new string[] {
                    "Touch the dark block during the simulation."
                };
            case 7:
                return new string[] {
                    "Each simulation start spends one point of energy.",
                    "You can see the remaining energy under the start button.",
                    "But it is not difficult to restore it.",
                    "Do not be afraid of experiments!"
                };
            case 8:
                return new string[] {
                    "Use two fingers to move the camera."
                };
            case 10:
                return new string[] {
                    "Meet the Gravity Block!",
                    "When the disk flies past this block - the direction of gravity changes."
                };
            case 14:
                return new string[] {
                    "And this is the Force Block.",
                    "He pushes the disc in the direction of the arrow."
                };
            case 16:
                return new string[] {
                    "And this is the Acceleration Block.",
                    "Flying past this block, the disk accelerates"
                };
            case 20:
                return new string[] {
                    "Teleport Block.",
                    "The disc moves from the teleport input to the output."
                };

            default:
                return new string[0];
        }



    }
}
