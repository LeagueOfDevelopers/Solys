using System.Collections.Generic;

public static class HelperTextData{

    public static string[] GetRussian(int index)
    {
        

        switch(index)
        {
            case 3:
                return new string[] {
                    "Добро пожаловать в Solus! Нажмите на эту подсказку для продолжения.",
                    "Нарисуйте линию, что бы диск попал в портал!",
                    "После этого запустите симуляцию нажав на кнопку в левом нижнем углу."
                };
            case 4:
                return new string[]{
                    "Вы можете использовать ластик что бы исправить небольшие недочёты"
                };
            case 5:
                return new string[]{
                    "Для рисования линий используется шкала энергии.",
                    "Ваш результат зависит от количества потраченной энергии.",
                    "Чем больше энергии останется - тем лучше!"
                };
            case 6:
                return new string[] {
                    "Коснитесь тёмного блока во время симуляции!"
                };


            default:
                return new string[0];
        }
            
        
        
    }

}
