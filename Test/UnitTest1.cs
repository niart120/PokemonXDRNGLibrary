
namespace Test
{
    public class UnitTest1
    {
        public static object[][] TestCases = new[]
        {
            new object[]{ 
                new XDQuickBattleArguments(PlayerTeam.�f�I�L�V�X, EnemyTeam.�T���_�[, 257, 648, 326, 281), 
                new XDQuickBattleArguments(PlayerTeam.�W���[�`, EnemyTeam.�T���_�[, 349, 325, 336, 313),
                new uint[] { 0x233F7EC1u, 0xF03F7EC1u }
            },
            new object[]{
                new XDQuickBattleArguments(PlayerTeam.�f�I�L�V�X, EnemyTeam.�t�@�C���[, 256, 650, 327, 256),
                new XDQuickBattleArguments(PlayerTeam.�~���E�c�[, EnemyTeam.�t���[�U�[, 362, 349, 320, 388),
                new uint[] { 0x4D1FFF4Du }
            },
            new object[]
            {
                new XDQuickBattleArguments(PlayerTeam.�~���E, EnemyTeam.�t���[�U�[, 340, 335, 309, 344),
                new XDQuickBattleArguments(PlayerTeam.�~���E, EnemyTeam.���e�B�I�X, 357, 321, 289, 290),
                new uint[] { 0x9F297767, 0x51297767, 0x03297767 }
            }
        };

        private static readonly XDDBClient client = new();

        [Theory]
        [MemberData(nameof(TestCases))]
        public void Test1(XDQuickBattleArguments first, XDQuickBattleArguments second, IEnumerable<uint> expected)
        {
            var res = client.Search(first, second);
            Assert.Equal(expected.OrderBy(_ => _), res.OrderBy(_ => _));
        }
    }
}