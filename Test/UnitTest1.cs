
namespace Test
{
    public class UnitTest1
    {
        public static object[][] TestCases = new[]
        {
            new object[]{
                new QuickBattleInput(PlayerTeam.�f�I�L�V�X, EnemyTeam.�T���_�[, 257, 648, 326, 281),
                new QuickBattleInput(PlayerTeam.�W���[�`, EnemyTeam.�T���_�[, 349, 325, 336, 313),
                new uint[] { 0x233F7EC1u, 0xF03F7EC1u }
            },
            new object[]{
                new QuickBattleInput(PlayerTeam.�f�I�L�V�X, EnemyTeam.�t�@�C���[, 256, 650, 327, 256),
                new QuickBattleInput(PlayerTeam.�~���E�c�[, EnemyTeam.�t���[�U�[, 362, 349, 320, 388),
                new uint[] { 0x4D1FFF4Du }
            },
            new object[]
            {
                new QuickBattleInput(PlayerTeam.�~���E, EnemyTeam.�t���[�U�[, 340, 335, 309, 344),
                new QuickBattleInput(PlayerTeam.�~���E, EnemyTeam.���e�B�A�X, 357, 321, 289, 290),
                new uint[] { 0x9F297767, 0x51297767, 0x03297767 }
            }
        };

        private static readonly XDDBClient client = new();

        [Theory]
        [MemberData(nameof(TestCases))]
        public void Test1(QuickBattleInput first, QuickBattleInput second, IEnumerable<uint> expected)
        {
            var res = client.Search(first, second);
            Assert.Equal(expected.OrderBy(_ => _), res.OrderBy(_ => _));
        }

        [Fact]
        public void Test2()
        {
            var searcher = new QuickBattleSeedSearcher(client);
            var res = searcher.Next(new QuickBattleInput(PlayerTeam.�~���E, EnemyTeam.�t���[�U�[, 340, 335, 309, 344));
            Assert.Equal(res, Array.Empty<uint>());
            res = searcher.Next(new QuickBattleInput(PlayerTeam.�~���E, EnemyTeam.���e�B�A�X, 357, 321, 289, 290));
            Assert.Equal(res.OrderBy(_ => _), new uint[] { 0x9F297767, 0x51297767, 0x03297767 }.OrderBy(_ => _));
            res = searcher.Next(new QuickBattleInput(PlayerTeam.���b�N�E�U, EnemyTeam.�T���_�[, 347, 241, 321, 293));
            Assert.Equal(res.OrderBy(_ => _), new uint[] { 0x1257DEF0 }.OrderBy(_ => _));
        }
    }

    public class GenerationTest
    {
        private static readonly XDDarkPokemon _zapdos, _articuno, _dragonite, _ralts;
        static GenerationTest()
        {
            _zapdos = XDRNGSystem.GetDarkPokemon("�T���_�[");
            foreach (var pre in _zapdos.PreGeneratePokemons.OfType<PreGenerateDarkPokemon>())
                pre.isFixed = true;

            _articuno = XDRNGSystem.GetDarkPokemon("�t���[�U�[");
            _dragonite = XDRNGSystem.GetDarkPokemon("�J�C�����[");
            _ralts = XDRNGSystem.GetDarkPokemon("�����g�X");
        }

        [Theory, 
            InlineData(0x604DB56Eu),
            InlineData(0xD4E241C7u),
            InlineData(0x37E1798Au)]
        public void TestGenerateFixedZapdos(uint seed)
        {
            var result = _zapdos.Generate(seed);
            Assert.Equal(0x3F3C705Eu, result.PID);
            Assert.Equal(new uint[] { 30, 31, 31, 30, 31, 31 }, result.IVs);
        }

        [Theory, 
            InlineData(0xE06758FDu),
            InlineData(0xF9B0EF42u),
            InlineData(0x68031769u)]
        public void TestGenerateArticuno(uint seed)
        {
            var result = _articuno.Generate(seed);
            Assert.Equal(0xC351DEF2u, result.PID);
            Assert.Equal(new uint[] { 31, 31, 31, 31, 31, 31 }, result.IVs);
        }

        [Theory,
            InlineData(0x00F402D5u),
            InlineData(0xBBCAB518u),
            InlineData(0x6F0240B9u)]
        public void TestGenerateDragonite(uint seed)
        {
            var result = _dragonite.Generate(seed);
            Assert.Equal(0xB1961329u, result.PID);
            Assert.Equal(new uint[] { 31, 31, 31, 4, 31, 31 }, result.IVs);
        }

        [Theory,
            InlineData(0x0671D11Du)]
        public void TestGenerateShinyBlockedRalts(uint seed)
        {
            var notBlocked = _ralts.Generate(seed);
            Assert.Equal(0x9D8707EBu, notBlocked.PID);
            Assert.Equal(new uint[] { 31, 0, 31, 31, 31, 31 }, notBlocked.IVs);

            var blocked = _ralts.Generate(seed, 39528);
            Assert.Equal(0xED1D5D5Bu, blocked.PID);
            Assert.Equal(new uint[] { 31, 0, 31, 31, 31, 31 }, blocked.IVs);
        }

    }

    public class ReverseTest
    {
        [Fact]
        public void TestReverseDragonite()
        {
            var dragonite = XDRNGSystem.GetDarkPokemon("�J�C�����[");
            var results = dragonite.CalcBack(31, 31, 31, 4, 31, 31);

            Assert.Equal(1, results.Count);

            var result = results[0];
            Assert.Equal(0xB1961329u, result.targetIndividual.PID);
            Assert.Equal(new uint[] { 31, 31, 31, 4, 31, 31 }, result.targetIndividual.IVs);
            Assert.Equal(280, result.generatableSeeds.Length);
            Assert.All(result.generatableSeeds, (_) =>
            {
                Assert.Equal(0xB1961329u, dragonite.Generate(_).PID);
            });
        }
    }

}
