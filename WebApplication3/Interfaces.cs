using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;

namespace WebApplication3
{
    [ServiceContract]
    public interface IRestCheckersService
    {
        /******  REST calls ******/
        // Players REST calls
        [OperationContract]
        [WebGet(UriTemplate = "/players/login?user={name}&password={password}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Player LoginWeb(string name, string password);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/players/add", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool AddPlayer(Player player);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/players/delete?playerId={playerId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool RemovePlayer(string playerId);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/players/update", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool UpdatePlayer(Player player);

        [OperationContract]
        [WebGet(UriTemplate = "/players/get?playerId={playerId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Player GetPlayerById(string playerId);

        [OperationContract]
        [WebGet(UriTemplate = "/players/getAll", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        List<Player> GetPlayers();

        [OperationContract]
        [WebGet(UriTemplate = "/players/byGame?gameId={gameId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        List<Player> GetPlayersByGame(string gameId);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/games/delete?gameId={gameId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool RemoveGame(string gameId);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/games/update", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool UpdateGame(Game game);

        // Games REST Calls
        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/games/add", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool AddGame(Game game);

        [OperationContract]
        [WebGet(UriTemplate = "/games/getAll", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        List<Game> GetGames();

        [OperationContract]
        [WebGet(UriTemplate = "/games/get?gameId={gameId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Game GetGameById(string gameId);

        [OperationContract]
        [WebGet(UriTemplate = "/games/byPlayer?playerId={playerId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        List<Game> GetGamesByPlayerId(string playerId);


        [OperationContract]
        [WebGet(UriTemplate = "/games/count?playerId={playerId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        int GetTotalGamesCountForPlayer(string playerId);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "/families/add", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool AddFamily(Family family);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "/families/delete?familyId={familyId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool DeleteFamily(string familyId);

        [OperationContract]
        [WebGet(UriTemplate = "/families/get?familyId={familyId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Family GetFamily(string familyId);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/families/update", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool UpdateFamily(Family family);

        [OperationContract]
        [WebGet(UriTemplate = "/families/byPlayer?playerId={familyId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        List<Player> GetPlayersByFamily(string familyId);

        [OperationContract]
        [WebGet(UriTemplate = "/moves/byGame?gameId={gameId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        List<Move> GetMovesByGame(string gameId);

    }

    [DataContract]
    public enum Status
    {
        [EnumMember]
        MOVE_ACCEPTED,
        [EnumMember]
        GAME_LOSE,
        [EnumMember]
        GAME_WIN,
        [EnumMember]
        NEW_GAME,
        [EnumMember]
        GAME_STARTED,
        [EnumMember]
        GAME_COMPLETED,
        [EnumMember]
        NO_SUCH_GAME,
        [EnumMember]
        WAITING_FOR_OTHER_PLAYER_TO_ARRIVE,
        [EnumMember]
        NOT_ENOUGH_PLAYERS_TO_START_GAME,
        [EnumMember]
        GAME_ALREADY_STARTED_BY_OTHER_PLAYERS,
        [EnumMember]
        NOT_LOGGED_IN,
        [EnumMember]
        NO_SUCH_USER,
        [EnumMember]
        WRONG_INPUT,
        [EnumMember]
        LOGIN_SUCCEDED

    }

    [DataContract]
    public class Player
    {

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Password { set; get; }
        [DataMember]
        public Family Family { get; set; }
    }

    [DataContract]
    public class Game
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public DateTime CreatedDateTime { get; set; }
        [DataMember]
        public Player Player1 { get; set; }
        [DataMember]
        public Player Player2 { get; set; }
        [DataMember]
        public Status GameStatus { get; set; }
        [DataMember]
        public int WinnerPlayerNum { get; set; }
    }

    [DataContract]
    public class Family
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }

    }

    [DataContract]
    public class Move
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int GameId { get; set; }
        [DataMember]
        public int PlayerId { get; set; }
        [DataMember]
        public DateTime DateTime { get; set; }
        [DataMember]
        public Coordinate From { get; set; }
        [DataMember]
        public Coordinate To { get; set; }
        
    }

    [DataContract]
    public class Coordinate
    {
        [DataMember]
        public int X { get; set; } // Rows
        [DataMember]
        public int Y { get; set; } // Columns

    }
    public class RestApiCalls
    {
        private WebChannelFactory<IRestCheckersService> MakeChannel()
        {
            Uri address = new Uri("http://localhost:52958/Service.svc/web");
            WebChannelFactory<IRestCheckersService> channel = new WebChannelFactory<IRestCheckersService>(address);
            return channel;
        }

        public Player LoginWeb(String name, String password)
        {
            WebChannelFactory<IRestCheckersService> channel = MakeChannel();
            Player result = null;
            try
            {
                channel.Open();
                IRestCheckersService c = channel.CreateChannel();
                result = c.LoginWeb(name,password);
                channel.Close();
                return result;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return result;
        }

        public List<Player> GetPlayers()
        {
            WebChannelFactory<IRestCheckersService> channel = MakeChannel();
            List<Player> result = new List<Player>();
            try
            {
                channel.Open();
                IRestCheckersService c = channel.CreateChannel();
                result = c.GetPlayers();
                channel.Close();
                return result;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return result;
        }

        public List<Game> GetGames()
        {
            WebChannelFactory<IRestCheckersService> channel = MakeChannel();
            List<Game> result = new List<Game>();
            try
            {
                channel.Open();
                IRestCheckersService c = channel.CreateChannel();
                result = c.GetGames();
                channel.Close();
                return result;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return result;
        }

        public List<Game> GetGameByPlayerId(string playerId)
        {
            WebChannelFactory<IRestCheckersService> channel = MakeChannel();
            List<Game> result = new List<Game>();
            try
            {
                channel.Open();
                IRestCheckersService c = channel.CreateChannel();
                result = c.GetGamesByPlayerId(playerId);
                channel.Close();
                return result;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return result;
        }

        public List<Player> GetPlayersByGameId(string gameId)
        {
            WebChannelFactory<IRestCheckersService> channel = MakeChannel();
            List<Player> result = new List<Player>();
            try
            {
                channel.Open();
                IRestCheckersService c = channel.CreateChannel();
                result = c.GetPlayersByGame(gameId);
                channel.Close();
                return result;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return result;
        }

        public int GetTotalGamesCountForPlayer(string playerId)
        {
            WebChannelFactory<IRestCheckersService> channel = MakeChannel();
            int result = 0;
            try
            {
                channel.Open();
                IRestCheckersService c = channel.CreateChannel();
                result = c.GetTotalGamesCountForPlayer(playerId);
                channel.Close();
                return result;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return result;
        }

        public bool RemovePlayer(string playerId)
        {
            WebChannelFactory<IRestCheckersService> channel = MakeChannel();
            bool result = false;
            try
            {
                channel.Open();
                IRestCheckersService c = channel.CreateChannel();
                result = c.RemovePlayer(playerId);
                channel.Close();
                return result;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return result;
        }

        public bool RemoveGame(string gameId)
        {
            WebChannelFactory<IRestCheckersService> channel = MakeChannel();
            bool result = false;
            try
            {
                channel.Open();
                IRestCheckersService c = channel.CreateChannel();
                result = c.RemoveGame(gameId);
                channel.Close();
                return result;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return result;
        }

        public bool UpdateGame(Game game)
        {
            WebChannelFactory<IRestCheckersService> channel = MakeChannel();
            bool result = false;
            try
            {
                channel.Open();
                IRestCheckersService c = channel.CreateChannel();
                result = c.UpdateGame(game);
                channel.Close();
                return result;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return result;
        }

        public bool UpdatePlayer(Player player)
        {
            WebChannelFactory<IRestCheckersService> channel = MakeChannel();
            bool result = false;
            try
            {
                channel.Open();
                IRestCheckersService c = channel.CreateChannel();
                result = c.UpdatePlayer(player);
                channel.Close();
                return result;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return result;
        }

        public Player GetPlayerById(string id)
        {
            WebChannelFactory<IRestCheckersService> channel = MakeChannel();
            Player result = null;
            try
            {
                channel.Open();
                IRestCheckersService c = channel.CreateChannel();
                result = c.GetPlayerById(id);
                channel.Close();
                return result;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return result;
        }

        public bool AddGame(Game game)
        {
            WebChannelFactory<IRestCheckersService> channel = MakeChannel();
            bool result = false;
            try
            {
                channel.Open();
                IRestCheckersService c = channel.CreateChannel();
                result = c.AddGame(game);
                channel.Close();
                return result;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return result;
        }

        public bool AddPlayer(Player player)
        {
            WebChannelFactory<IRestCheckersService> channel = MakeChannel();
            bool result = false;
            try
            {
                channel.Open();
                IRestCheckersService c = channel.CreateChannel();
                result = c.AddPlayer(player);
                channel.Close();
                return result;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return result;
        }

        public bool AddFamily(Family family)
        {
            WebChannelFactory<IRestCheckersService> channel = MakeChannel();
            bool result = false;
            try
            {
                channel.Open();
                IRestCheckersService c = channel.CreateChannel();
                result = c.AddFamily(family);
                channel.Close();
                return result;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return result;
        }
    }
}