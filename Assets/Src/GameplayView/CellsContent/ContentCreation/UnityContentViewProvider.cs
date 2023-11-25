﻿using castledice_game_logic.GameObjects;
using UnityEngine;
using Tree = castledice_game_logic.GameObjects.Tree;
using CastleGO = castledice_game_logic.GameObjects.Castle;

namespace Src.GameplayView.CellsContent.ContentCreation
{
    public class UnityContentViewProvider : MonoBehaviour, IContentViewProvider, IContentVisitor<ContentView>
    {
        private IPlayerContentViewPrefabProvider _playerContentViewPrefabProvider;
        private ICommonContentViewPrefabProvider _commonContentViewPrefabProvider;
        
        public void Init(IPlayerContentViewPrefabProvider playerContentViewPrefabProvider, ICommonContentViewPrefabProvider commonContentViewPrefabProvider)
        {
            _playerContentViewPrefabProvider = playerContentViewPrefabProvider;
            _commonContentViewPrefabProvider = commonContentViewPrefabProvider;
        }
        
        public ContentView GetContentView(Content content)
        {
            return content.Accept(this);
        }

        public ContentView VisitTree(Tree tree)
        {
            var prefab = _commonContentViewPrefabProvider.TreePrefab;
            var treeView = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            treeView.Init(tree, new GameObject());
            return treeView;
        }

        public ContentView VisitCastle(CastleGO castle)
        {
            var prefab = _playerContentViewPrefabProvider.GetCastlePrefab(castle.GetOwner());
            var castleView = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            castleView.Init(castle);
            return castleView;
        }

        public ContentView VisitKnight(Knight knight)
        {
            var prefab = _playerContentViewPrefabProvider.GetKnightPrefab(knight.GetOwner());
            var knightView = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            knightView.Init(knight, new GameObject(), Vector3.zero);
            return knightView;
        }
    }
}