
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
}