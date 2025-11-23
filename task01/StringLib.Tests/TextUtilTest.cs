using StringLib;

namespace StringLib.Tests;

public class TextUtilTest
{
    [Theory]
    [MemberData(nameof(SplitIntoWordParams))]
    public void Can_split_into_words(string input, string[] expected)
    {
        List<string> result = TextUtil.SplitIntoWords(input);
        Assert.Equal(expected, result);
    }

    [Theory]
    [MemberData(nameof(ReverseWordsParams))]
    public void Can_reverse_words(string input, string expected)
    {
        string result = TextUtil.ReverseWords(input);
        Assert.Equal(expected, result);
    }

    public static TheoryData<string, string[]> SplitIntoWordParams()
    {
        return new TheoryData<string, string[]>
        {
            // Апостроф считается частью слова
            { "Can't do that", ["Can't", "do", "that"] },

            // Буква "Ё" считается частью слова
            { "Ёжик в тумане", ["Ёжик", "в", "тумане"] },
            { "Уж замуж невтерпёж", ["Уж", "замуж", "невтерпёж"] },

            // Дефис в середине считается частью слова
            { "Что-нибудь хорошее", ["Что-нибудь", "хорошее"] },
            { "mother-in-law's", ["mother-in-law's"] },
            { "up-to-date", ["up-to-date"] },
            { "Привет-пока", ["Привет-пока"] },

            // Слова из одной буквы допускаются
            { "Ну и о чём речь?", ["Ну", "и", "о", "чём", "речь"] },

            // Смена регистра не мешает разделению на слова
            { "HeLLo WoRLd", ["HeLLo", "WoRLd"] },
            { "UpperCamelCase or lowerCamelCase?", ["UpperCamelCase", "or", "lowerCamelCase"] },

            // Цифры не считаются частью слова
            { "word123", ["word"] },
            { "123word", ["word"] },
            { "word123abc", ["word", "abc"] },

            // Знаки препинания не считаются частью слова
            { "C# is awesome", ["C", "is", "awesome"] },
            { "Hello, мир!", ["Hello", "мир"] },
            { "Много   пробелов", ["Много", "пробелов"] },

            // Пустые строки, пробелы, знаки препинания
            { null!, [] },
            { "", [] },
            { "   \t\n", [] },
            { "!@#$%^&*() 12345", [] },
            { "\"", [] },

            // Пограничные случаи с апострофами и дефисами
            { "-привет", ["привет"] },
            { "привет-", ["привет"] },
            { "'hello", ["hello"] },
            { "hello'", ["hello"] },
            { "--привет--", ["привет"] },
            { "''hello''", ["hello"] },
            { "'a-b'", ["a-b"] },
            { "--", [] },
            { "'", [] },
        };
    }

    public static TheoryData<string, string> ReverseWordsParams()
    {
        return new TheoryData<string, string>
        {
            // Основные примеры
        {
            "The quick brown fox jumps over the lazy dog",
            "ehT kciuq nworb xof spmuj revo eht yzal god"
        },
        {
            "Статья 1.2.1 пункт 8.",
            "яьтатС 1.2.1 ткнуп 8."
        },

        // === АНГЛИЙСКИЙ ЯЗЫК ===
        // Простые слова
        { "Hello", "olleH" },
        { "Hello World", "olleH dlroW" },
        { "ABC DEF", "CBA FED" },

        // Сохранение регистра
        { "HeLLo WoRLd", "oLLeH dLRoW" },
        { "UPPERCASE lowercase", "ESACREPPU esacrewol" },
        { "MiXeD CaSe", "DeXiM eSaC" },

        // === РУССКИЙ ЯЗЫК ===
        { "Привет мир", "тевирП рим" },
        { "Ёжик в тумане", "кижЁ в енамут" },
        { "Уж замуж невтерпёж", "жУ жумаз жёпретвен" },

        // Русский регистр
        { "Привет Мир", "тевирП риМ" },
        { "ПРИВЕТ мир", "ТЕВИРП рим" },

        // === СМЕШАННЫЕ ЯЗЫКИ ===
        { "Hello мир", "olleH рим" },
        { "Привет world", "тевирП dlrow" },
        { "C# и Java", "C# и avaJ" },

        // Цифры внутри слов
        { "word123", "drow123" },
        { "123word", "123drow" },
        { "word123abc", "drow123cba" },
        { "abc123def456", "cba123fed456" },

        // Цифры отдельно
        { "123 456 789", "123 456 789" },
        { "version 1.2.3", "noisrev 1.2.3" },
        { "item 42 is answer", "meti 42 si rewsna" },

        // === ЗНАКИ ПРЕПИНАНИЯ ===
        { "Hello, world!", "olleH, dlrow!" },
        { "C# is awesome!", "C# si emosewa!" },
        { "What? Why! Yes.", "tahW? yhW! seY." },
        { "test... end", "tset... dne" },

        // Сложные случаи с пунктуацией
        { "\"Hello,\" he said.", "\"olleH,\" eh dias." },
        { "Don't; wait: now!", "t'noD; tiaw: won!" },
        { "price: $100.00", "ecirp: $100.00" },
        { "email@example.com", "liame@elpmaxe.moc" },
        { "https://example.com", "sptth://elpmaxe.moc" },

        // Множественные пробелы
        { "Много   пробелов", "огонМ   волеборп" },
        { "a  b   c    d", "a  b   c    d" },
        { "  start  end  ", "  trats  dne  " },

        // Табы и новые строки
        { "line1\tline2", "enil1\tenil2" },
        { "first\nsecond", "tsrif\ndnoces" },
        { "a\tb\tc", "a\tb\tc" },

        // Пустые строки
        { "", "" },
        { "   ", "   " },
        { "\t\n", "\t\n" },

        // Только символы (без букв)
        { "!@#$%^&*()", "!@#$%^&*()" },
        { "... --- ...", "... --- ..." },

        // Слова из одной буквы
        { "I am a programmer", "I ma a remmargorp" },
        { "Ну и о чём речь?", "уН и о мёч ьчер?" },
        { "A B C D E", "A B C D E" },

        // CamelCase
        { "UpperCamelCase", "esaClemaCreppU" },
        { "lowerCamelCase", "esaClemaCrewol" },
        { "XML Parser HTTP Request", "LMX resraP PTTH tseuqeR" },

        // Аббревиатуры
        { "USA UK EU", "ASU KU UE" },
        { "NASA FBI CIA", "ASAN IBF AIC" },

        // Крайние случаи
        { "--", "--" },
        { "''", "''" },
        { "-", "-" },
        { "'", "'" },

        // === НЕСТАНДАРТНЫЕ СИМВОЛЫ ===
        { "★hello★", "★olleh★" },
        { "←left right→", "←tfel thgir→" },
        { "emoji 😊 test", "ijome 😊 tset" },

        // === ДЛИННЫЕ ТЕКСТЫ ===
        { "Supercalifragilisticexpialidocious", "suoicodilaipxecitsiligarfilacrepuS" },
        { "Это очень длинное слово для тестирования", "отЭ ьнечо еоннилд оволс ялд яинаворитсет" },
        };
    }
}